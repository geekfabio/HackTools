import psutil

pid=input("digite o pid1:")
p = psutil.Process(pid)
p.suspend()

pid=input("digite o pid2:")
p = psutil.Process(pid)
p.suspend()

i = input("ok")

