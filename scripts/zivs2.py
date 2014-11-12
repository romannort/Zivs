#!/usr/bin/env python

import sys
import math
import json
import base64

#decode args
decoded_args = base64.b64decode(sys.argv[1])
json_arg = json.loads(decoded_args)

# key #1
a = int(json_arg['a'])
# key #2
b = int(json_arg['b'])
# p
p = int(json_arg['p'])
# m - message
m = json_arg['m']
# step {2|3} - number of step where we got our message 
step = int(json_arg['s'])
#library - array of words
library = [str(s) for s in json_arg['l']]

def strToInt(m):
	return [ord(c) for c in m]

def intToStr(m):
	s = ''
	for i in m:
		s += chr(i)

	return s

def isCrossSimple(a, b):
	for i in range(2, int(math.sqrt(max(a, b)))+1):
		if(a % i == 0 and b % i == 0):
			return False

	return True

def getSecond(x, p):
	for i in range(2, p):
		if(x*i % (p-1) == 1):
			return i

	return None

def isM3():
	global step
	return step == 3

def getMessage(a, b, alpha, m):
	im = strToInt(m)
	res = []
	for i in range(len(im)):
		c = pow(im[i], a) % p
		c = pow(c, b) % p
		if isM3():
			c = pow(c, alpha) % p
		res.append(int(c))

	return res

def getOutput(a, b, p, m):
	alpha = getSecond(a, p)
	betha = getSecond(b, p)

	if(alpha == None or betha == None):
		return None

	res = []
	for i in range(len(m)):
		c = m[i]
		if not isM3():
			c = pow(m[i], alpha) % p
		c = pow(c, betha) % p
		res.append(int(c))

	return [a, alpha, b, betha, intToStr(res)]

def checkOut(out):
	global library

	if out == None:
		return False

	m = out[4].split()
	for word in m:
		if not (word in library):
			return False

	return True

def hack(m, p):
	res = []
	for a in range(3, p):
		for b in range(a, p):
			if isCrossSimple(a, b):
				out = getOutput(a, b, p, m)

				if checkOut(out):
					res.append(out)
	return res

recievedMessage = getMessage(a, b, getSecond(a, p), m)

# print as list of [a, alpha, b, betha, mu4]
results = hack(recievedMessage, p)
json_results = []
for res in results:
	json_results.append({"a": res[0], "af": res[1], "b": res[2], "bt": res[3], "m": res[4]})

print base64.b64encode(json.dumps(json_results))