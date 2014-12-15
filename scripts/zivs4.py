#!/usr/bin/env python

import sys
import math
import json
import base64
import numpy as np

poly_len = int(sys.argv[1])

def generateFirstState(poly_len):
	firstState = [0 for i in range(poly_len)]
	firstState[0] = 1

	return firstState

firstState = generateFirstState(poly_len)

def generateStates(state):
	newStateStart = 0
	for i in range(1, len(state)):
		newStateStart ^= state[i]

	newState = [newStateStart]
	for i in range(0, len(state)-1):
		newState.append(state[i])

	global firstState
	if firstState == newState:
		return []
	else:
		states = generateStates(newState)
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
		T[getDecimal(states[i])][getDecimal(states[i+1])] = 1
	T[getDecimal(states[len(states)-1])][getDecimal(states[0])] = 1

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

	twos = []

	for i in range(poly_len):
		twos.append(2**i)

	for i in range(2**poly_len):
		if ((not i in twos and L.item(rowIndex, i) != 0) or (i in twos and L.item(rowIndex, i) == 0)):
			return False

	return True

states = generateStates(firstState)
states.append(firstState)

states = states[::-1]

# print "\nStates:"
# print states

T = generateTMatrix(states, 2**poly_len)

# print "\nMatrix T:"
# print T

notBinC = generateCMatrix(2**poly_len)

# print "\nMatrix C:"
# print C

C = np.matrix([[notBinC.item(i, j) % 2 for j in range(len(notBinC))] for i in range(len(notBinC))])

# print "\nMatrix C as bin:"
# print C

transposedT = T.transpose()

# print "\nMatrix T transposed:"
# print transposedT

L = np.dot(np.dot(transposedT, C), transposedT)

# print "\nMatrix L:"
# print L

isLinear = checkLinearity(L, poly_len)
if isLinear:
	print "Linear"
else:
	print "Not Linear"