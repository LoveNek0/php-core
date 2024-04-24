<?php

$result1 = 10 + 5;
$result2 = 20 - 8;
$result3 = 6 * 4;
$result4 = 50 / 2;

$a = 10;
$b = $a + 5;

$addition = function($x, $y) {
    return $x + $y;
};
$result5 = $addition(3, 7);

$subtract = fn($x, $y) => $x - $y;
$result6 = $subtract(10, 3);

$array1 = [1, 2, 3];
$array2 = ['a', 'b', 'c'];

$mergedArray = array_merge($array1, $array2);

$element = $mergedArray[2];

$mergedArray[] = 'd';

unset($mergedArray[1]);

$arrayLength = count($mergedArray);
