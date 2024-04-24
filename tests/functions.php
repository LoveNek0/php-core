<?php

function a($a, $b) {
    return $a + $b;
}

function b(int $a){
    return $a * b;
}

function c(float $a = 53, int &...$b){
    return $a + $b[0];
}

$_a = a(1, 2);
$_b = b(1, 2);
$_c = c(1, 2, 3);

$d = fn($a, $b) => $a + $b;
$e = fn(int $a, float &...$b) => $a * $b[0];

$d(1, 2);
$e(1, 2, 3);

$f = function ($a, $b) {
    return $a + $b;
};
$f(1, 2);
