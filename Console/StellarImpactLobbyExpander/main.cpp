#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <tchar.h>

#define BUTTON_ID      1001
void OnCreate(HWND hWnd);
void OnCommand(HWND hWnd, WPARAM wParam);
static TCHAR szWindowClass[] = _T("win32app");
static TCHAR szTitle[] = _T("Win32 Guided Tour Application");
HINSTANCE hInst;
LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM);

int WINAPI WinMain(HINSTANCE hInstance,
                   HINSTANCE hPrevInstance,
                   LPSTR lpCmdLine,
                   int nCmdShow)
{
    WNDCLASSEX wcex;

    wcex.cbSize = sizeof(WNDCLASSEX);
    wcex.style          = CS_HREDRAW | CS_VREDRAW;
    wcex.lpfnWndProc    = WndProc;
    wcex.cbClsExtra     = 0;
    wcex.cbWndExtra     = 0;
    wcex.hInstance      = hInstance;
    wcex.hIcon          = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_APPLICATION));
    wcex.hCursor        = LoadCursor(NULL, IDC_ARROW);
    wcex.hbrBackground  = (HBRUSH)(COLOR_WINDOW+1);
    wcex.lpszMenuName   = NULL;
    wcex.lpszClassName  = szWindowClass;
    wcex.hIconSm        = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_APPLICATION));

    if (!RegisterClassEx(&wcex))
    {
        MessageBox(NULL,
            _T("Call to RegisterClassEx failed!"),
            _T("Win32 Guided Tour"),
            NULL);

        return 1;
    }

    hInst = hInstance; // Store instance handle in our global variable

    HWND hWnd = CreateWindow(
        szWindowClass,
        szTitle,
        WS_OVERLAPPEDWINDOW,
        CW_USEDEFAULT, CW_USEDEFAULT,
        500, 100,
        NULL,
        NULL,
        hInstance,
        NULL
    );

    if (!hWnd)
    {
        MessageBox(NULL,
            _T("Call to CreateWindow failed!"),
            _T("Win32 Guided Tour"),
            NULL);

        return 1;
    }

    ShowWindow(hWnd,
        nCmdShow);
    UpdateWindow(hWnd);

    // Main message loop:
    MSG msg;
    while (GetMessage(&msg, NULL, 0, 0))
    {
        TranslateMessage(&msg);
        DispatchMessage(&msg);
    }

    return (int) msg.wParam;
}
static HWND hButton;
TCHAR* greeting;

void OnCommand(HWND hWnd, WPARAM wParam)
{
	switch (wParam)
	{
		case BUTTON_ID:
			greeting = _T("Clicked");
			break;
		default:
			break;
	}

}

void OnCreate(HWND hWnd)
{
	hButton = CreateWindow( "button", "Label",
        WS_CHILD | WS_VISIBLE | BS_DEFPUSHBUTTON,
        100, 200, 
        50, 20,
        hWnd, (HMENU) BUTTON_ID,
        hInst, NULL );
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    PAINTSTRUCT ps;
    HDC hdc;
    greeting = _T("Hello, World!");

    switch (message)
    {
		case WM_PAINT:
			hdc = BeginPaint(hWnd, &ps);
			TextOut(hdc,
				5, 5,
				greeting, _tcslen(greeting));
			// End application-specific layout section.

			EndPaint(hWnd, &ps);
			break;
		case WM_COMMAND: 
			OnCommand(hWnd, wParam);
			break;
		case WM_CREATE:
      
			OnCreate(hWnd);
			break;
		case WM_DESTROY:
			PostQuitMessage(0);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
			break;
    }

    return 0;
}

