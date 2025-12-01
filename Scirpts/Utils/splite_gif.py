import os
import sys
from PIL import Image, ImageSequence


class GifSpliter:
    def __init__(self, gif_file_path, save_path="../../Resources/Images/"):
        if not os.path.exists(gif_file_path):
            raise OSError("The GIF file is not existed...")
        # self.gif = Image.open(gif_file_path)
        # self.folder_name = os.path.basename(gif_file_path)
        if not os.path.exists(save_path):
            os.makedirs(save_path)
        self.info = f""
        self.gif_list = self._init_gif_file_list(gif_file_path)
        self.save_path = save_path

    # def _get_info(self):
    def _init_gif_file_list(self, gif_file_path):
        if not os.path.exists(gif_file_path):
            raise OSError(f"The gif file path {gif_file_path} is not exists.... check your gif file path")
        
        if os.path.isdir(gif_file_path):
            return self._init_gif_file_list_from_folder(gif_file_path)
        else:
            return self._init_gif_file_list_from_file(gif_file_path)

    def _init_gif_file_list_from_folder(self, gif_foler_path):
        return [os.path.join(gif_foler_path, path) for path in os.listdir(gif_foler_path) if path.endswith('.gif')]
    
    def _init_gif_file_list_from_file(self, gif_file_path: str):
        if not gif_file_path.endswith('.gif'):
            print(f'| Warning | The gif file should endswith .gif, not {gif_file_path.split('.')[-1]}')
            return []
        else:
            return [gif_file_path]




    def _split_single_gif(self, file_path):
        file_name = os.path.basename(file_path).split('.')[0]
        target_path = os.path.join(self.save_path, file_name)
        print(target_path)
        if not os.path.exists(target_path):
            os.makedirs(target_path)
        gif_file = Image.open(file_path)
        iter = ImageSequence.Iterator(gif_file)
        index = 0
        for frame in iter:
            frame.save(os.path.join(target_path, f'{index}.png'))
            index += 1
        # print(file_name)


    def _splie_all_gif_file(self):
        pass


if __name__ == '__main__':
    gifSpliter = GifSpliter('C:/Users/gyc20/Desktop/PVZ史上最完整素材（B站）/植物大战僵尸素材包第二版/植物/')
    for path in gifSpliter.gif_list:
        print(path)
        gifSpliter._split_single_gif(path)
