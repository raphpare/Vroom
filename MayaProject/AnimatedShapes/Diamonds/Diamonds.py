# Animation avec des keyframes des polySpheres ayant la forme de diamants
# Création de deux classes: Keyframe et Timeline
# Utilisation de dictionnaire pour conserver les données initiales des diamants
# Génération procédurale de cinq polySheres en forme de diamant

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


def draw_diamond_and_timeline(diamond_information, timeline_name):
    cmds.polySphere(
        name=diamond_information['name'],
        radius=diamond_information['radius'],
        subdivisionsX = 3,
        subdivisionsY = 3
    )

    new_timeline = Timeline(timeline_name, diamond_information['name'], 0)

    new_timeline.add_keyframes_for_timeframe(0, [{ 'translateY' : diamond_information['translateY1'] }, { 'rotateY' : 0 }])
    new_timeline.add_keyframes_for_timeframe(30, [{ 'translateY' : diamond_information['translateY2'] }, { 'rotateY' : 90 }])
    new_timeline.add_keyframes_for_timeframe(60, [{ 'translateY' : diamond_information['translateY1'] }, { 'rotateY' : 180 }])
    new_timeline.add_keyframes_for_timeframe(90, [{ 'translateY' : diamond_information['translateY2'] }, { 'rotateY' : 270 }])
    new_timeline.add_keyframes_for_timeframe(120, [{ 'translateY' : diamond_information['translateY1'] }, { 'rotateY' : 360 }])

    new_timeline.bake()
    new_timeline.set_timeframe_start() 
    new_timeline.set_timeframe_end()

initial_information_diamond_1 = {
    'name' : 'diamond1',
    'radius' : 2,
    'translateY1' : 5,
    'translateY2' : 4
}

initial_information_diamond_2 = {
    'name' : 'diamond2',
    'radius' : 0.5,
    'translateY1' : 2,
    'translateY2' : 1
}

initial_information_diamond_3 = {
    'name' : 'diamond3',
    'radius' : 0.5,
    'translateY1' : 8,
    'translateY2' : 7
}

initial_information_diamond_4 = {
    'name' : 'diamond4',
    'radius' : 2,
    'translateY1' : 11,
    'translateY2' : 10
}

initial_information_diamond_5 = {
    'name' : 'diamond5',
    'radius' : 0.5,
    'translateY1' : 14,
    'translateY2' : 13
}

draw_diamond_and_timeline(initial_information_diamond_1, 'TIMELINE_DIAMOND_1')
draw_diamond_and_timeline(initial_information_diamond_2, 'TIMELINE_DIAMOND_2')
draw_diamond_and_timeline(initial_information_diamond_3, 'TIMELINE_DIAMOND_3')
draw_diamond_and_timeline(initial_information_diamond_4, 'TIMELINE_DIAMOND_4')
draw_diamond_and_timeline(initial_information_diamond_5, 'TIMELINE_DIAMOND_5')