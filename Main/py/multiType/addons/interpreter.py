
import sys


none = "NONE"

class Interpreter:
    def __init__(self):
        self.syntaxs = ["sync",";","to","kill"]

        pass

    def _getLastIndex(self, List):
        return list(enumerate(List))[::-1][0][0]

    def _SplitBySyntax(self,prompt:str):
        promptList:list[str] = []
        if not prompt.strip() == "":
            spaceSplit = prompt.split(" ")
            for Index,word in enumerate(spaceSplit):
                if word.lower() in self.syntaxs:
                    promptList.append(word)
                else:
                    if not Index == 0 and not spaceSplit[Index-1].lower() in self.syntaxs:
                        promptList.append(spaceSplit[Index-1]+" "+word)
                    elif not Index == self._getLastIndex(spaceSplit) and spaceSplit[Index+1].lower() in self.syntaxs:
                        promptList.append(word)
        newList = []
        for i in promptList:
            if not i.strip() == "":
                newList.append(i)
        return newList

    def _lowerList(self,List:list[str]):
        return [i.lower() for i in List]

    def _FindIndex(self,value:str,List:list[str],start:int = 0, stop = sys.maxsize):
        index = None
        try:
            index = self._lowerList(List).index(value.lower(),start,stop)
        except ValueError:
            pass
        return index


    def interprete(self,inputPrompt):
        codeDicts = []
        if not inputPrompt.strip() == "":
            prompts = [inputPrompt] if not ";" in inputPrompt else inputPrompt.split(";")
            for prompt in prompts:
                promptList = self._SplitBySyntax(prompt)
                codeDict = {}
                """ print(prompt)
                print(promptList) """
                if promptList[0].lower() == "sync":
                    funcIndex = self._FindIndex("sync",promptList)
                    # print(funcIndex)
                    if not funcIndex == None:
                        # print("ffff")
                        to  = promptList[self._FindIndex("to",promptList)+1] if "to" in self._lowerList(promptList) else ""
                        codeDict["sync"] = {
                            "filename":promptList[funcIndex+1] if not promptList[funcIndex+1].lower() in self.syntaxs else None
                            ,"to":[
                                to] if not "," in to else to.split(",")    
                        }
                if promptList[0].lower() == "kill":
                    funcIndex = self._FindIndex("kill",promptList)
                    if not funcIndex == None:
                        ids  = promptList[self._FindIndex("kill",promptList)+1] if "kill" in self._lowerList(promptList) else ""
                        codeDict["kill"] = {
                            "ids":[
                                ids] if not "," in ids else ids.split(",")    
                        }
                codeDicts.append(codeDict)
        return codeDicts
    

if __name__ == "__main__":
    exe = Interpreter()
    print(exe.interprete("sync 'file/path' to 'new f/file/path','dfddfdf/dfd'; kill 'F122B3C','dfddff' "))
    LiteralOutput = [{'sync': {'filename': "'file/path'", 'to': ["'new f/file/path'", "'dfddfdf/dfd'"]}}, {'kill': {'ids': ["'F122B3C'", "'dfddff' "]}}]
