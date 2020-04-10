$REGASM_PATH="REGASM"
$EXE_PATH="EXE"
Start-Sleep -s 15
Start-Process -FilePath $REGASM_PATH -ArgumentList "/codebase" , $EXE_PATH  -Wait
$EXE_PATH
