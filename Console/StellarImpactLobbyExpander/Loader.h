#pragma once
#include <iostream>
#define _WIN32_WINNT 0x500  
#include <Windows.h>
#include <string>
#include <ctime>
#include <limits>
#include <sstream>
 
class Loader
{

public:
	Loader(void);
	void Load(void);
	virtual ~Loader(void);

private:
	void WriteToMemory(HANDLE hProcHandle);
	void ValueToByte(int val);
	void CharToByte(char* chars, byte* bytes, unsigned int count);
	DWORD FindDmaAddress(int PointerLevel, HANDLE hProcHandle, DWORD Offsets[], DWORD BaseAddress);
};

