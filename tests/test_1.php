<?php

// Пример кода со всеми операторами
$a = 5;
$b = 10;
$c = $a + $b * 2 - 3 / 4 % 5 ** 6;
for ($i = 0; $i < 5; $i++) {
    foreach ($arr as $key => $value) {
        while ($j < 10) {
            do {
                echo 'Value: ' . $value . '\n';
            } while ($j < 5);
        }
    }
}
$d = $a;
$d += $b;
$d -= $c;
$d *= $a;
$d /= $b;
$d %= $c;
$d **= $a;
$d .= $a . $b . $c;
if ($a == $b) {
    echo 'a is equal to b\n';
} elseif ($a === $b) {
    echo 'a is identical to b\n';
} else {
    echo 'a is not equal to b\n';
}
if ($a != $b) {
    echo 'a is not equal to b\n';
} elseif ($a <> $b) {
    echo 'a is not equal to b using <> operator\n';
} else {
    echo 'a is equal to b\n';
}
if ($a !== $b) {
    echo 'a is not identical to b\n';
} else {
    echo 'a is identical to b\n';
}
if ($a > $b) {
    echo 'a is greater than b\n';
} elseif ($a < $b) {
    echo 'a is less than b\n';
} elseif ($a >= $b) {
    echo 'a is greater than or equal to b\n';
} elseif ($a <= $b) {
    echo 'a is less than or equal to b\n';
} elseif ($a <=> $b) {
    echo 'a is not equal to b using spaceship operator\n';
}

?>
