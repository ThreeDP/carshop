#!/bin/sh

HOST="http://localhost:5103/"
PATH_OUT="./outs/"
PATH_EXP="./cases/"

mkdir -p $PATH_OUT

# TEST GET CLIENTES
{
    EXP=$PATH_EXP"test_getclientes.json"
    OUT=$PATH_OUT"out_getclientes.json"

    curl -X GET $HOST"clientes" > $OUT; diff $EXP $OUT 
}


rm -rf $PATH_OUT