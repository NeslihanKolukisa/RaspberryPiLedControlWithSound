from urllib.request import urlopen
import xmltodict
import json
#!/usr/bin/python
import RPi.GPIO as GPIO
GPIO.setwarnings(False)
GPIO.setmode(GPIO.BCM)
GPIO.setup(26, GPIO.OUT)
GPIO.setup(19, GPIO.OUT)
GPIO.setup(13, GPIO.OUT)

def homepage():
    file = urlopen('http://tubitak.ozkanunsal.com/WebService1.asmx/VeriList')
    data = file.read()
    file.close()
    data = xmltodict.parse(data)
    output= json.loads(json.dumps(data))
    deger=output["string"]["#text"]
    print(deger)
    if(deger.find('kirmizi')!=-1):
        GPIO.output(26,GPIO.HIGH)
    if(deger.find('yesil')!=-1 and deger.find('kapat')==-1):
        GPIO.output(13,GPIO.HIGH)
    if(deger.find('Sari')!=-1):
        GPIO.output(19,GPIO.HIGH)
    if(deger.find('Kirmizi')!=-1 and deger.find('kapat')!=-1):
        GPIO.output(26,GPIO.LOW)
    if(deger.find('sari')!=-1 and deger.find('kapat')!=-1):
        GPIO.output(19,GPIO.LOW)
    if(deger.find('yesil')!=-1 and deger.find('kapat')!=-1):
        GPIO.output(13,GPIO.LOW)
    if(deger.find('tümünü aç')!=-1):
        GPIO.output(26,GPIO.HIGH)
        GPIO.output(13,GPIO.HIGH)
        GPIO.output(19,GPIO.HIGH)
    if(deger.find('Tümünü kapat')!=-1):
        GPIO.output(26,GPIO.LOW)
        GPIO.output(13,GPIO.LOW)
        GPIO.output(19,GPIO.LOW)
       

while 1==1:
    homepage()
