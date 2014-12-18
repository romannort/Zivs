#!/usr/bin/env python

import sys
import math
import json
import base64
import numpy as np

poly = sys.argv[1]
poly_len = len(poly)

def printAll(matrix):
	for i in range(len(matrix)):
		if(i < 10):
			print i, '  ',
		else:
			print i, ' ',

		for j in range(len(matrix)):
			print matrix.item(i, j),
		print ""

def generateFirstState(poly_len):
	firstState = [0 for i in range(poly_len)]
	firstState[0] = 1

	return firstState

firstState = generateFirstState(poly_len)

def generateStates(poly, state):
	newStateStart = 0
	for i in range(len(state)):
		if poly[i] == "1":
			newStateStart ^= state[i]

	newState = [newStateStart]
	for i in range(0, len(state)-1):
		newState.append(state[i])

	global firstState
	if firstState == newState:
		return []
	else:
		states = generateStates(poly, newState)
		states.append(newState)
		return states

def getDecimal(state):
	res = 0
	length = len(state)
	for i in range(length):
		res = res + state[i] * (2**(length - i - 1))
	return res

def generateTMatrix(states, size):
	T = [[0 for x in range(size)] for x in range(size)]
	for i in range(len(states) - 1):
		T[getDecimal(states[i+1])][getDecimal(states[i])] = 1
	T[getDecimal(states[0])][getDecimal(states[len(states)-1])] = 1

	return np.matrix(T)

def getFactorial(n):
	if n < 2:
		return 1
	else:
		return n * getFactorial(n - 1);

def generateCMatrix(size):
	C = [[0 for x in range(size)] for x in range(size)]

	for i in range(size):
		for j in range(i+1):
			C[i][j] = (getFactorial(i) / (getFactorial(j) * getFactorial(i - j)))

	return np.matrix(C)

def checkLinearity(L, poly_len):
	rowIndex = 2**(poly_len - 1)

	global poly
	twos = []
	for i in range(len(poly)):
		if(poly[i] == "1"):
			twos.append(2 ** (i+1))

	for i in twos:
		print i, L.item(rowIndex, i-1)
		if (L.item(rowIndex, i-1) == 0):
			return False

	return True

def cellMul(size, a, b, x, y):
	result = 0
	for i in range(size):
		result = result + a.item(x, i) * b.item(i, y)

	return result % 2

def mul(a, b):
	size = len(a)
	res = [[0 for x in range(size)] for x in range(size)]
	for i in range(size):
		for j in range(size):
			res[i][j] = cellMul(size, a, b, i, j)

	return np.matrix(res)

states = generateStates(poly, firstState)
states.append(firstState)

states = states[::-1]

print "\nStates:"
print np.matrix(states)

T = generateTMatrix(states, 2**poly_len)

# print "\nMatrix T:"
# printAll(T)

notBinC = generateCMatrix(2**poly_len)

C = notBinC % 2

transposedC = C.transpose()

# print "\nMatrix C:"
# printAll(C)

# print "\nMatrix C Transposed:"
# print transposedC

tempL = mul(transposedC, T)

# print "\nMatrix L temp:"
# printAll(tempL)
L = mul(tempL, transposedC)


print "\nMatrix L:"
printAll(L)

# isLinear = checkLinearity(L, poly_len)
# if isLinear:
# 	print "Linear"
# else:
# 	print "Not Linear"