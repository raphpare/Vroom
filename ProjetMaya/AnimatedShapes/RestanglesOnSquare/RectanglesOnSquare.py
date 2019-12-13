# Animation avec des keyframes de deux rectangles d'un cube
# Création de deux classes: Keyframe et Timeline
# Génération procédurale de deux rectangles et d'un cube

import maya.cmds as cmds

class Keyframe:
    def __init__(self, time, attribute, value):
        self.time = time
        self.attribute = attribute
        self.value = value

class Timeline:
    def __init__(self, name, target, start):
        self.name = name
        self.target = target
        self.keyframes = []
        self.timeframe_start = 0
        self.timeframe_end = None

    def add_keyframes_for_timeframe(self, timeframe, keyframes):
        if len(keyframes) != 0:
            for keyframe in keyframes:            
                for key in keyframe:
                    kf =  Keyframe(timeframe, key, keyframe[key])
                    self.keyframes.append(kf)

    def bake(self):
        if self.target is not None:
            for keyframe in self.keyframes:
                cmds.setKeyframe(
                self.target,
                time = keyframe.time,
                attribute = keyframe.attribute,
                value = keyframe.value)
    
    def set_timeframe_start(self, timeframe = 0):
        self.timeframe_start = timeframe
        cmds.playbackOptions(edit=True, minTime=timeframe)
    
    def get_timeframe_start(self):
        return self.timeframe_start

    def set_timeframe_end(self, timeframe = None):
        if timeframe == None:
            timeframe = self.keyframes[len(self.keyframes) - 1].time
        self.timeframe_end = timeframe
        cmds.playbackOptions(edit=True, maxTime=timeframe)

    def get_timeframe_end(self):
        return self.timeframe_end


# Dessinner le Cube 1 et sa ligne du temps
cmds.polyCube(
    name='Cube1',
    width=2,
    height=2,
    depth=2
)
              
timeline_cube_1 = Timeline('TIMELINE_CUBE_1', 'Cube1', 0)
timeline_cube_1.add_keyframes_for_timeframe(0, [{ 'scaleY' : 0.5 }, { 'translateY' : 3.5 }, { 'rotateY' : 360 }])
timeline_cube_1.add_keyframes_for_timeframe(30, [{ 'scaleY' : 1.5 }, { 'rotateY' : 180 }])
timeline_cube_1.add_keyframes_for_timeframe(60, [{ 'scaleY' : 1.5 }, { 'rotateY' : 90 }])
timeline_cube_1.add_keyframes_for_timeframe(100, [{ 'scaleY' : 0.5 }, { 'rotateY' : 0 }])
timeline_cube_1.bake()

# Dessinner le Cube 2 et sa ligne du temps
cmds.polyCube(
    name='Cube2',
    width=2,
    height=2,
    depth=2
)
              
timeline_cube_2 = Timeline('TIMELINE_CUBE_2', 'Cube2', 0)
timeline_cube_2.add_keyframes_for_timeframe(0, [{ 'translateY' : 1 }, { 'rotateY' : 360 }])
timeline_cube_2.add_keyframes_for_timeframe(30, [{ 'rotateY' : 180 }])
timeline_cube_2.add_keyframes_for_timeframe(60, [{ 'rotateY' : 90 }])
timeline_cube_2.add_keyframes_for_timeframe(100, [{ 'rotateY' : 0 }])
timeline_cube_2.bake()

# Dessinner le Cube 3 et sa ligne du temps
cmds.polyCube(
    name='Cube3',
    width=0.5,
    height=0.5,
    depth=0.5
)
              
timeline_cube_3 = Timeline('TIMELINE_CUBE_3', 'Cube3', 0)
timeline_cube_3.add_keyframes_for_timeframe(0, [{ 'translateY' : 2.5 }, { 'rotateY' : 360 }])
timeline_cube_3.add_keyframes_for_timeframe(30, [{ 'rotateY' : 180 }])
timeline_cube_3.add_keyframes_for_timeframe(60, [{ 'rotateY' : 90 }])
timeline_cube_3.add_keyframes_for_timeframe(100, [{ 'rotateY' : 0 }])
timeline_cube_3.bake()

timeline_cube_3.set_timeframe_start()
timeline_cube_3.set_timeframe_end()

cmds.hyperShade (assign = "particleCloud1")