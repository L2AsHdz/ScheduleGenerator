import json

def validar_horario(horario):
    aulas_ocupadas = {}
    profesores_ocupados = {}

    for curso in horario:
        aula = curso["salon"]["nombre"]
        hora_inicio = curso["horaInicio"]
        hora_fin = curso["horaFin"]
        profesor = curso["catedratico"]["nombre"]

        # Validar traslapes en aulas
        if aula in aulas_ocupadas:
            for rango in aulas_ocupadas[aula]:
                if (hora_inicio < rango[1] and hora_fin > rango[0]):
                    return False, f"Traslape en aula {aula} entre {curso['curso']['nombre']} y {rango[2]}"

        # Validar traslapes de profesores
        if profesor in profesores_ocupados:
            for rango in profesores_ocupados[profesor]:
                if (hora_inicio < rango[1] and hora_fin > rango[0]):
                    return False, f"Traslape de profesor {profesor} entre {curso['curso']['nombre']} y {rango[2]}"

        # Registrar el curso como ocupado
        if aula not in aulas_ocupadas:
            aulas_ocupadas[aula] = []
        aulas_ocupadas[aula].append((hora_inicio, hora_fin, curso["curso"]["nombre"]))

        if profesor not in profesores_ocupados:
            profesores_ocupados[profesor] = []
        profesores_ocupados[profesor].append((hora_inicio, hora_fin, curso["curso"]["nombre"]))

    return True, "Horario válido sin traslapes"

# Cargar el JSON desde el archivo
archivo_json = "horario.json"  # Cambia el nombre del archivo si es diferente

try:
    with open(archivo_json, "r") as archivo:
        horario_json = json.load(archivo)
except FileNotFoundError:
    print(f"No se encontró el archivo '{archivo_json}'. Asegúrate de que el archivo exista y esté en la ubicación correcta.")
    exit(1)

# Validar el horario
valido, mensaje = validar_horario(horario_json["horario"])
if valido:
    print("El horario es válido.")
else:
    print(f"El horario no es válido: {mensaje}")