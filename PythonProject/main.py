import cv2
import os
from PIL import Image

def detect_and_crop_faces(image_path, output_path):
    # Cargar el clasificador preentrenado para detección de rostros
    face_cascade = cv2.CascadeClassifier(cv2.data.haarcascades + 'haarcascade_frontalface_default.xml')

    # Leer la imagen
    image = cv2.imread(image_path)
    if image is None:
        return False  # Si la imagen no se pudo leer

    # Convertir a escala de grises
    gray_image = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

    # Detectar rostros
    faces = face_cascade.detectMultiScale(gray_image, scaleFactor=1.1, minNeighbors=5, minSize=(30, 30))

    # Si no se detectan rostros, retornar
    if len(faces) == 0:
        return False

    # Recortar el primer rostro detectado
    for (x, y, w, h) in faces:
        face = image[y:y+h, x:x+w]
        cropped_image = Image.fromarray(cv2.cvtColor(face, cv2.COLOR_BGR2RGB))
        cropped_image.save(output_path)
        return True

    return False


detect_and_crop_faces("Images/ImageTest.jpg", "Images/ImageTestCropped.jpg")