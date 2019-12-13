# Création la classe Podium qui desinne un podium
# Génération procédurale de cubes dans une bouble for
# Sélection d'un podium dans la scène pour le supprimer de la scène (cmds.delete())

import maya.cmds as cmds

class Podium:
    def __init__(self, name, nb_steps, step_witdh, step_height):
        self.name = name
        self.nb_steps = nb_steps
        self.step_witdh = step_witdh
        self.step_height = step_height
        self._steps_list = []
        self._first_step_witdh = self.step_witdh

    def draw(self, incrementation):
        if len(self._steps_list) > 0:
            print('Error: %s already draw') % self.name
            return

        self.step_witdh = self._first_step_witdh

        for step_index in range (self.nb_steps):
            name = '%s__step%i' % (self.name, step_index + 1)

            self.step_witdh = self.step_witdh if step_index <= 0 else self.step_witdh + incrementation

            self.__draw_step(name)

            move_y = self.step_height / 2 - step_index * self.step_height
            cmds.move(0, move_y, 0)

            self._steps_list.append(name)

        if len(self._steps_list) > 1:
            cmds.polyUnite(self._steps_list, name=self.name)

        cmds.select(clear=True)

    def delete(self):
        if len(self._steps_list) <= 0:
            self.__get_not_found_error_message()
            return

        self.__select(self.name)
        cmds.delete()

        self.__select(self._steps_list)
        cmds.delete()
        self._steps_list = []

    def __draw_step(self, name):
        cmds.polyCube(
            name=name,
            width=self.step_witdh,
            height=self.step_height,
            depth=self.step_witdh)
    
    def __select(self, selected_element_name):
        if len(self._steps_list) <= 0:
            self.__get_not_found_error_message()
            return []

        cmds.select(clear=True)
        cmds.select(selected_element_name, add=True)
        return cmds.ls(selection = True)

    def __get_not_found_error_message(self):
        print("Error: %s not found. Try %s.draw() method") % (self.name, self.name)

podium_name = 'Podium'
nb_podium_steps = 60
first_podium_step_witdh = 12
step_height = 0.6
incrementation = 3

podium = Podium(
    podium_name,
    nb_podium_steps,
    first_podium_step_witdh,
    step_height)

podium.draw(incrementation)

