# M150_EncryptionProject
## Prozess
Wir hatten die Idee ein verschlüsselungs Programm zu machen. Für dieses Programm wollen wir C# verwenden. Also machten wir uns auf die Suche nach einem passendem Nuget Package. Wir haben dann auch eins gefunden, aber dieses hatte einen Fehler ausgelöst welchen wir nicht beheben konnten. Also haben wir weiter gesucht und ein gutes Package gefunden. Mit diesem hatten wir weniger Probleme und konnten somit unsere Funktion gut implementieren.
## Beschreibung
Unser Programm ist ein Verschlüsselungsprogramm. Man kann eine Datei mit einem Schlüssel zusammen verschlüsseln und auch wieder mit dem gleichen Schlüssel entschlüsseln. Wir haben zwei verschiedene verschlüsselungen gebrauch. Einmal Cäsar Cypher und danach eine Hash Funktion.
## Verwendung
Um eine Datei zu verschlüsseln muss man in unserem Programm diese Datei auswählen und einen beliebigen Key auswählen. Anschliessen klickt man auf "encrypt", jetzt wählt man den Speicherort für die verschlüsselte Datei.
Um die Datei wieder zu entschlüsseln Wählt man die verschlüsselte Datei und gibt den zuvor eingegebenen Key ein und klickt auf "decrypt", jetzt noch den Speicherort wählen und schon kann man den Dateiinhalt wieder ansehen.
## Probleme
- Beim Cäsar Cypher gibt es das Problem das es teilweise einzelne Buchstaben nicht richtig zurück entschlüsselt.
- Wir hatten start Schwierigkeiten mit unserem ersten nuget Package, haben es dann eigentlich richtig implementiert, dann hatte es aber einen Fehler gegeben welchen wir nicht beheben konnten.
## Verbesserungen
Wenn wir noch mehr Zeit gehabt hätten, hätten wir noch folgendes gemacht:
- Performance verbessert
- Mehr Tests gemacht um den einen Bug noch zu entfernen
- Eine weitere Verschlüsselung hinzugefügt
## Reflexion
Im Grossen und Ganzen ist uns das Projekt gelungen, mit mehr Zeit hätten wir es noch besser machen können und mit einer besseren Planung hätten wir auch in diesem Zeitrahmen noch mehr machen können.