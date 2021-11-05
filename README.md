# M150_EncryptionProject
## Prozess
Wir hatten die Idee ein verschlüsselungs Programm zu machen. Für dieses Programm wollen wir C# verwenden und mit dem nuget Package "Effortless.Net.Encryption" arbeiten.Bevor wir dieses Package gefunden haben, haben wir versucht mit einem anderen Package die Verschlüsselung umzusetzen, jedoch ohne erfolg.
## Verwendung
Um eine Datei zu verschlüsseln muss man in unserem Programm diese Datei auswählen und einen beliebigen Key auswählen. Anschliessen klickt man auf "encrypt", jetzt wählt man den Speicherort für die verschlüsselte Datei.
Um die Datei wieder zu entschlüsseln Wählt man die verschlüsselte Datei und gibt den zuvor eingegebenen Key ein und klickt auf "decrypt", jetzt noch den Speicherort wählen und schon kann man den Dateiinhalt wieder ansehen.
## Probleme
Beim Cäsar Cypher gibt es das Problem das es teilweise einzelne Buchstaben nicht richtig zurück entschlüsselt.
Wir hatten start schwierigkeiten mit unserem ersten nuget Package, haben es dann eigentlich richtig implementiert, dann hatte es aber einen Fehler gegeben welchen wir nicht beheben konnten.