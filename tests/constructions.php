<?php
$a = 1;
$b = 6;

while($a < $b)
    $a += 0.5;
while($b <= $a ){
    $b += 0.5;
    echo $a . $b;
}

do
    echo $a;
while($a < $b);
do{
    echo $b;
    $b++;
}
while($b <= $a);

for($a = 1; $a < 6; $a += 0.5)
    echo $a;
for(; $b <= $a; $b += 0.5){
    echo $b;
    print $b . $a;
}

foreach($a as $k)
    echo $a;
foreach($b as $k => $v){
    print $k;
    print $v;
    var_dump($k, $v);
}

if(isset($a) && $a > $b){
    print $a + $b;
    return $a;
}
else
    echo $b;
