# e-Zustellung AddIn für Microsoft Word 2010 und 2013

Das e-Zustellung AddIn für Microsoft Word 2012 und 2013 ist eine Proof-Of-Concept 
Implementierung die den Versand von Schriftstücken über die Remote Control Schnittstelle 
der e-Zustellung gemäß den Spezifikationen [AustriaPro](http://www.ezustellung.at)
durchzuführen. Die Software besteht aus zwei Teilen:
1. Das Desktop Versand Programm
2. Das Word AddIn

Installation
------------
1. Laden Sie das Zertifikat von labs1.austriapro.at mit Hilfe des Browsers herunter und installieren sie das Zertifikat
2. Laden SIe den Source Code von github herunter und erstellen Sie die Module, wählen Sie beim WordAddInPOC Projekt "Veröffentlichen" in das Verzeichnis C:\OfficeApps\eZustellung\WordAddInPOC
3. Kopieren Sie das bin/Release Verezeichnis des Projektes eZustellPOC in das Verzeichnis C:\OfficeApps\eZustellung
4. Starten Sie die Installation für das Word AddIn im Verzeichnis C:\OfficeApps\eZustellung WordAddInPOC

Verwendung des Versandmoduls
----------------------------

1. Starten Sie das Programm eZustellPOC im Verzeichnis C:\OfficeApps\eZustellung
2. Erstellen sie ein SW-Zertifikat oder laden es aus dem Dateisystem
3. Öffnen sie das Adressbuch

Hinweis: Im Verzeichnis C:\OfficeApps\eZustellung\Log werden die Logdateien abgelegt und können zur
Problemeingrenzung mit jedem Texteditor geöffnet werden.

Verwendung des Word AddIn
-------------------------

1. Starten Sie Word
2. Öffnen Sie ein Dokument oder erstellen Sie ein neues Dokument
3. Klicken Sie im Word Ribbon auf eZustellung und dort auf "Mit eZustellung versenden"
4. Verfahren Sie dann so wie bei "Verwendung des Versandmoduls" beschrieben
