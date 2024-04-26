<?php

function a($a, $b) {
    return $a + $b;
}

function b(int $a): array{
    return $a * b;
}

function c(float $a = 53, int &...$b): CustomClass{
    return $a + $b[0];
}

$_a = a(1, 2);
$_b = b(1, 2);
$_c = c(1, 2, 3);

$d = fn($a, $b) => $a + $b;
$e = fn&(int $a, float &...$b): float => $a * $b[0];

$d(1, 2);
$e(1, 2, 3);

$f = function ($a, $b) {
    return $a + $b;
};
$g = function& ($a, $b): string {
    return $a + $b;
};
$f(1, 2);
$g(1, 2);
