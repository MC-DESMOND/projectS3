import random
from customtkinter import CTk
from tkinter import Tk
import edge



class TkWindow(Tk):
    def __init__(self,title = __name__,width = 300,height = 300,x = 10,y = 10,**kwargs):
        super().__init__(screenName=title)
        self.config(kwargs)
        self.isIconified = False
        self.title(title)
        
        self._IntervalId = ""
        self._onExit = lambda e:None
        self._IntervalRun = False
        self.onExitDestroyWin = True
        self.toogleIconify = self.wm_toogleIconify
        self.geometry(f"{width}x{height}+{x}+{y}")
        
  
    @property
    def centerScreenPos(self):
        pos = edge.Class(typename= "center",attr={"x": int((self.ScreenWidth()/2)-(self.Width()/2)),"y":int( (self.ScreenHeight()/2)-(self.Height()/2))})
        return pos
    @property
    def centerWindowPos(self):
        pos = edge.Class(typename= "center",attr={"x": int((self.Width()/2)),"y":int( (self.Height()/2))})
        return pos


    def wm_iconify(self):
        self.isIconified = True
        return super().wm_iconify()
    
    def wm_deiconify(self):
        self.isIconified = False
        return super().wm_deiconify()

    def wm_toogleIconify(self):
        if self.isIconified:
            self.wm_deiconify()
        else:
            self.wm_iconify()

    def ToolWindow(self,allow = True):
        self.attributes(
            "-toolwindow",
            allow,
        )
    def TopMost(self,allow = True):
        self.attributes(
            "-topmost",
            allow,
        )
    def FullScreen(self,allow = True):
        self.attributes(
            "-fullscreen",
            allow,
        )
    def OverrideRedirect(self,allow = True):
        self.wm_overrideredirect(allow)
    
    def Transparency(self,color = ""):
        self.attributes(
            "-transparentcolor",
            color,
        )

    def Exit(self):
        self._onExit()

    def onKeyPress(self,event):
        print(event)

    def onChange(self,event):
        print(event)

    @property
    def onExit(self):
        return self._onExit

    @onExit.setter
    def onExit(self,Func):
        self._onExit = edge.mergeFunc(Func,lambda :self.destroy() if self.onExitDestroyWin else None)
        self.wm_protocol("WM_DELETE_WINDOW", self._onExit)

    def ScreenWidth(self):
        self.update()
        return int(self.winfo_screenwidth())

    def ScreenHeight(self):
        self.update()
        return int(self.winfo_screenheight())
    
    @property
    def Width(self):
        self.update()
        return int(self.winfo_width())

    @Width.setter
    def Width(self,val):
        self.geometry(f"{val}x{self.Height}+{self.X}+{self.Y}")

    @property
    def Height(self):
        self.update()
        return int(self.winfo_height())
    
    @Height.setter
    def Height(self,val):
        self.geometry(f"{self.Width}x{val}+{self.X}+{self.Y}")
        
    @property
    def X(self):
        self.update()
        return self.geometry().split("+")[1]
    
    @X.setter
    def X(self,val):
        self.geometry(f"{self.Width}x{self.Height}+{val}+{self.Y}")
    
    @property
    def Y(self):
        self.update()
        return self.geometry().split("+")[2]
    
    @Y.setter
    def Y(self,val):
        self.geometry(f"{self.Width}x{self.Height}+{self.X}+{val}")

    def intervalFunc(self):
        pass
    
    def RunInterval(self,val:bool = True):
        self._IntervalRun = val

    def _Interval(self):
        if self._IntervalRun:
            self.intervalFunc()
        self._IntervalId = self.after(1,self._Interval)
        
    def mainloop(self,customLoopFunc:bool  = None, n = 0):
        self.bind("<KeyPress>", self.onKeyPress)
        self.bind("<Configure>", self.onChange)
        self.update()
        self._Interval()
        return (
            super().mainloop(n)
            if not customLoopFunc 
            else customLoopFunc 
            if type(customLoopFunc) == bool 
            else customLoopFunc(n)
            )



if __name__ == "__main__":
    win = TkWindow("Test",bg = "black")
    win.intervalFunc  = lambda : print(random.randint(0,200))
    win.onChange = lambda e:print("Changed")
    win.onExit = lambda:print("Exited")
    win.mainloop()