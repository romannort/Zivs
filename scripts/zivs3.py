#!/usr/bin/env python2
#coding: utf-8

import sys
import json
import base64
import operator

separator = '01'

def generateCodes(count):
	codes = []
	codes.append('0')
	codes.append('1')
	current_step = codes
	global separator
	while(len(codes) < count):
		new_codes = []
		for code in current_step:
			if(not separator in (code + '0')):
				new_codes.append(code + '0')
			if(not separator in (code + '1')):
				new_codes.append(code + '1')
		current_step = new_codes
		for new_code in new_codes:
			codes.append(new_code)

	return codes

def generateABC(text):
	frequency = {}
	abc = {}

	for c in text:
		if c in frequency:
			frequency[c] += 1
		else:
			frequency[c] = 1

	codes = generateCodes(len(frequency))
	frequency = [key for (key, value) in sorted(frequency.items(), key=operator.itemgetter(1), reverse=True)]

	for i in range(len(frequency)):
		abc[frequency[i]] = codes[i]

	return abc

def revertJSON(json_abc):
	abc = {}

	for key in json_abc:
		abc[str(json_abc[key])] = key

	return abc

def encodeText(text, abc):
	global separator
	encoded_text = ""

	for c in text:
		encoded_text = encoded_text + separator + abc[c]

	coef = 100 * (float(len(encoded_text)) / (len(text) * 8)) 

	return json.dumps({"text" : encoded_text, "abc" : json.dumps(abc), "coef" : coef})

def decodeText(text, abc):
	global separator
	decoded_text = ""

	chars = text.split(separator)
	chars.pop(0)

	for c in chars:
		decoded_text += abc[str(c)]
	return json.dumps({"text" : decoded_text})

def main():

	# decode args
	decoded_args = base64.b64decode(sys.argv[1])
	json_arg = json.loads(decoded_args)

	encode = json_arg['encode']
	text = json_arg['text']
	result = ""

	if encode:
		abc = generateABC(text)
		result =  encodeText(text, abc)
	else:
		abc = revertJSON(json_arg['abc'])
		result =  decodeText(text, abc)

	print base64.b64encode(result)

main()