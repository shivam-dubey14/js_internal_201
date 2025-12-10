function fun(x){
    return x*2 ;
}
ch = fun(23);


let arr = [1,2,3,4,5,"apple","banana",ch];



let fruits = ["banana","apple","guvava","pineapple","lemon"];

// for(let i = 0 ; i<fruits.length ; i++){
//     console.log(fruits[i]);
// }

for(let a of fruits){
    console.log(a);
}

fruits.push('Papaya');
console.log(fruits);

fruits.unshift('kiwi');
console.log(fruits);

console.log(fruits.includes("Mango"));

console.log(fruits.indexOf("banana"));

let arr2 = [2,4,5,6,8];
let ar3 = arr2.map(n=>n*2);
console.log(ar3);

let ar4 = arr2.filter(n=>n>5);
console.log(ar4);

let ar5 = arr2.reduce((acc,curr) => acc+ curr ,  0);
console.log(ar5);
console.log("hey");





