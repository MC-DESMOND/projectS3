
from Window import TkWindow
import tkinter
import pyautogui as py
import colour

class App(TkWindow):
    def __init__(self):
        super().__init__()
        self.started = False
        self.title("Color Getter")
        self.ToolWindow()
        self.TopMost()
        self.resizable(False, False)
        self.labelHex = tkinter.Label(self,text="Color Hex",bg="black",fg="white",font=20)
        self.labelHex.pack(fill=tkinter.BOTH, expand=True)
        self.labelRGB = tkinter.Label(self,text="Color Rgb",bg="black",fg="white",font=20)
        self.labelRGB.pack(fill=tkinter.BOTH, expand=True)
        self.entryHex = tkinter.Entry(self,font=20,bg="black",fg="white",justify="center")
        self.entryHex.pack(fill=tkinter.BOTH, expand=True)
        self.entryRGB = tkinter.Entry(self,font=20,bg="black",fg="white",justify="center")
        self.entryRGB.pack(fill=tkinter.BOTH, expand=True)
        self.controlBtn = tkinter.Button(self,font=30,bg="black",text="Space Bar | START",fg="white",justify="center",command=self.start)
        self.controlBtn.pack(fill=tkinter.BOTH, expand=True)
        self.update()
        
    def start(self):
        self.started = True
        self.controlBtn.config(command=self.stop,text="Space Bar | STOP")
        self.RunInterval(self.started)

    def stop(self):
        self.started = False
        self.controlBtn.config(command=self.start,text="Space Bar | START")
        self.RunInterval(self.started)

    def onKeyPress(self, event):
        print(event)
        if str(event.keysym).lower() == "return":
            if self.started:
                self.stop()
            else:
                self.start()

    def invert_hex_color(self,hex_color: str) -> str:
        hex_color = hex_color.lstrip('#')
        inverted_color = '{:06X}'.format(0xFFFFFF - int(hex_color, 16))
        return f'#{inverted_color}'


    def intervalFunc(self):
        try:
            pos = py.position()
            rgb = py.pixel(pos.x,pos.y)
            Hex = colour.rgb2hex((i/255 for i in rgb))
            self.labelHex.config(text=f"Hex | {Hex}",bg=Hex, fg=self.invert_hex_color(Hex))
            self.entryHex.delete(0,tkinter.END)
            self.entryHex.insert(0,Hex)
            rgb =f'rgb({ str(",".join(map(str,rgb)))})'
            self.labelRGB.config(text=f"RGB | {rgb}",bg=Hex, fg=self.invert_hex_color(Hex))
            self.entryRGB.delete(0,tkinter.END)
            self.entryRGB.insert(0,rgb)
            
        except:
            self.labelHex.config(text="can't get color")
            self.labelRGB.config(text="can't get color")
         

App().mainloop()