from RestService.Rest import Rest
from BeaconScanner.BeaconTag import  BeaconTag
'''from BeaconScanner.BeaconScanner import  BeaconScanner
from MockUp import  ScannerMock'''
from Algorithms import TagAlgorithm
from kivy.app import App
from GUI.ComponentHandler import ComponentHandler


'''r= Rest()
r.GetImages("1")

s = BeaconScanner()
l =s.scan()

for o in l:
    print("adress: %s  signaal: %s" % (o.GetMac(), o.GetStrength()))
d= TagAlgorithm.TagAlgorithm.getClosestBeacon(l)

print("closest")
print("adress: %s  signaal: %s" % (d.GetMac(), d.GetStrength()))'''

#initializer
class Init(App):
    def build(self):
        #        TagAlgorithm.TagAlgorithm.getClosestBeacon()
        componentHandler=ComponentHandler("image")
        self.tag= BeaconTag("1",123)
        componentHandler.SetTag(self.tag)
        componentHandler.Start()
        component=componentHandler.GetComponent()
        Clock.schedule_interval(self.CheckNextBeacon, 1)
        return component

    def CheckNextBeacon(self):
        if self.componentHandler.TimeIsUp():
            print("loadnext")
        else:
            print("don't load next")
        return


Init().run()
