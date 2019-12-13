# Création la classe Columns qui desinne des colonnes
# Génération procédurale de polyCylinders
# Attribution par programmation d'une texture avec cmds.hyperShade()
# Sélection des colonnes dans la scène pour leur appliquer des transformations de mouvement (cmds.move()) ou pour les supprimer de la scène (cmds.delete())

import maya.cmds as cmds

class Columns:
    def __init__(self, name, nb_columns_in_x, nb_columns_in_z, column_diameter, column_height, distance_between_column):
        self.name = name
        self.nb_columns_in_x = int(nb_column_in_x)
        self.nb_columns_in_z = int(nb_columns_in_z)
        self.column_diameter = column_diameter
        self.column_radius = self.column_diameter/2
        self.column_height = column_height
        self.distance_between_column = distance_between_column
        self._columns_list = []

    def draw(self):
        if len(self._columns_list) > 0:
            print('Error: %s already draw') % self.name
            return

        row_x_index_counter = 0
        distance = self.distance_between_column + self.column_diameter
        
        for column_z_index in range(0, self.nb_columns_in_z):

            for row_x_index in range(0, self.nb_columns_in_x):

                has_column_on_x = row_x_index == 0 or row_x_index == self.nb_columns_in_x - 1
                has_column_on_z = column_z_index == 0 or column_z_index == self.nb_columns_in_z - 1

                if has_column_on_x or has_column_on_z:
                    column_name = '%s__col%i_row%i' % (self.name, column_z_index + 1, row_x_index + 1)
                    
                    self.__draw_column(column_name)

                    move_x = row_x_index_counter * distance
                    move_y = self.column_height/2
                    move_z = column_z_index * distance
                    cmds.move(move_x, move_y, move_z)

                    self._columns_list.append(column_name)
            
                if row_x_index >= self.nb_columns_in_x - 1:
                    row_x_index_counter = 0
                else: 
                    row_x_index_counter += 1
        
        if len(self._columns_list) > 1:
            cmds.polyUnite(self._columns_list, name=self.name)

        cmds.select(clear=True)

    def center_in_scene(self):
        selection = self.__select(self.name)

        if len(selection) <= 0:
            return

        size_max_x = cmds.getAttr('%s.boundingBoxMaxX' % selection[0])
        size_max_z = cmds.getAttr('%s.boundingBoxMaxZ' % selection[0])
        cmds.move(-(size_max_x - self.column_diameter)/2 - self.column_radius/2, 0, -(size_max_z - self.column_diameter)/2 - self.column_radius/2)
        
        cmds.select(clear=True)

    def move(self, position_x, position_y, position_z):
        selection = self.__select(self.name)

        if len(selection) <= 0:
            return
        
        cmds.move(position_x, position_y, position_z)
        
        cmds.select(clear=True)

    def delete(self):
        if len(self._columns_list) <= 0:
            self.__get_not_found_error_message()
            return

        self.__select(self.name)
        cmds.delete()

        self.__select(self._columns_list)
        cmds.delete()
        self._columns_list = []

    def __draw_column(self, name):
        cmds.polyCylinder(name=name,
            r=self.column_diameter/2,
            h=self.column_height,
            sx=30,
            sy=1,
            sz=0,
            ax=(0, 0, 0),
            rcp=0,
            cuv=3,
            ch=1)

    def __select(self, selected_element_name):
        if len(self._columns_list) <= 0:
            self.__get_not_found_error_message()
            return []

        cmds.select(clear=True)
        cmds.select(selected_element_name, add=True)
        return cmds.ls(selection = True)
    
    def __get_not_found_error_message(self):
        print("Error: %s not found. Try %s.draw() method") % (self.name, self.name)

columns_name= 'Columns'        
nb_column_in_x = 3
nb_columns_in_z = 3
column_diameter = 2
column_height = 24
distance_between_column = 18

columns = Columns(
    columns_name, 
    nb_column_in_x,
    nb_columns_in_z,
    column_diameter,
    column_height,
    distance_between_column)

columns.draw()
columns.center_in_scene()
