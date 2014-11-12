#!/usr/bin/env python
 
#1 000101101001101010110010011001001000000100001 ARMIY TARTU TIARA key: 100 111 101 110 000
#2 111101101001101010110010011001101111010011000 ?
#3 111101101001101111101011001000100000011100101 MARIY RACIY MICAR key: 001 100 000 101 101
#4 111101101001101111101000110111111101100001000 MARIY ARMIY RACIY key: 100 100 000 011 101
#5 011101101001101100000011111101111110000110111 MARTA MICAR ARMIY key: 001 010 000 011 101
#6 111101101001101001000101100001010000000001001 RACIY MICAR MARAT key: 101 110 100 111 001
#7 111101101001101100000100100101011111010100110 MARIY MICAR TIARA key: 010 001 011 000 101
 
from itertools import permutations
import sys
 
perms = [''.join(p) for p in permutations('MICARYTU')]
 
lib = ['ARMIY', 'MIRTA', 'MICAR', 'UTYTA', 'MARIY', 'CITRA', 'TARTU', 'RACIY', 'TRATA', 'MARTA', 'MARAT', 'TATRA', 'ARTUR', 'TIARA', 'ARAMA', 'TIMUR', 'MUMIY']
 
source = [[0 for x in range(5)] for x in range(3)]
 
def getBits(word, abc):
        res = [0 for x in range(len(word))]
        j = 0
       
        for i in range(0, len(word)):
                res[j] = abc.index(word[i])
                j = j + 1
       
        return res[::-1]
 
def getWord(bits, abc):
        res = ''
 
        for i in range(len(bits)):
                res = res + abc[bits[i]]
 
        return res[::-1]
 
def main():
        for i in range(len(lib)):
                # print i*100/len(lib), "%"
                for j in range(len(perms)):
                        word = getBits(lib[i], perms[j])
                        key = [a^b for a,b in zip(word, source[0])]
                        word1 = getWord([a^b for a,b in zip(key, source[1])], perms[j])
                        word2 = getWord([a^b for a,b in zip(key, source[2])], perms[j])
                        if (word1 in lib) and (word2 in lib):
                                print lib[i], word1, word2
                                print "key:",
                                for x in range(len(key)):
                                        print str(bin(key[x]))[2:].zfill(3),
                                print ""
                                break
        # print "Done"
 
inputString = str(sys.argv[1])
 
for i in range(0, 3):
        for j in range(0, 5):
                idx = (i*5+j)*3
                if(inputString[idx] == '1'):
                        source[i][j] = 4
                if(inputString[idx + 1] == '1'):
                        source[i][j] = source[i][j] + 2
                if(inputString[idx + 2] == '1'):
                        source[i][j] = source[i][j] + 1
 
main()