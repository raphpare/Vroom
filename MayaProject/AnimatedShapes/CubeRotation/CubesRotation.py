# Animation avec des keyframes de cubes en rotations
# Assignation et lecture d'attributs ('rotateX' et 'rotateZ') dans une scène Maya
# Sélection des cubes dans la scène pour leur appliquer les mêmes keyframes
# Génération de cubes d'un nombre aléatoire entre 12 et 16

import maya.cmds as cmds
import random

nb_max_cube = random.randrange(12, 16)

def get_attribute(nodeName, attributeName):
    if cmds.objExists('%s.%s' % (nodeName, attributeName)):
        return cmds.getAttr('%s.%s' % (nodeName, attributeName))
    else:
        return None

def set_attribute(nodeName, attributeName, attributeValue):
    if cmds.objExists('%s.%s' % (nodeName, attributeName)):
        cmds.setAttr('%s.%s' % (nodeName, attributeName), attributeValue)


for cube_index in range(1, nb_max_cube):
    cube_name = 'Cube%i' % (cube_index)
    new_cube = cmds.polyCube (
        name=cube_name,
        w = 2,
        h = 2,
        d = 2,
        subdivisionsX = 1,
        subdivisionsY = 1
    )

    cmds.hyperShade (assign = "lambert1")

    currentRotateY = get_attribute(cube_name, 'rotateX')
    currentRotateZ = get_attribute(cube_name, 'rotateZ')

    if currentRotateY is not None:  
        set_attribute(cube_name, 'rotateX', random.randrange(0, 360))

    if currentRotateZ is not None:  
        set_attribute(cube_name, 'rotateZ', random.randrange(0, 360))
   

cmds.select (all=True)
target = cmds.ls(selection=True)

if len(target) > 1:
    cmds.setKeyframe(target, time=0, attribute='rotateY', value=360.0)
    cmds.setKeyframe(target, time=0, attribute='translateY', value=0)

    cmds.setKeyframe(target, time=60, attribute='translateY', value=1.5)

    cmds.setKeyframe(target, time=120, attribute='rotateY', value=0.0)
    cmds.setKeyframe(target, time=120, attribute='translateY', value=0)  

    cmds.keyTangent (inTangentType='linear', outTangentType='linear')
    
    cmds.playbackOptions(edit=True, minTime=0)
    cmds.playbackOptions(edit=True, maxTime=120)

cmds.polyUnite(target, name='CubesRotation')