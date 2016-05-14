FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Paranthesis.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
