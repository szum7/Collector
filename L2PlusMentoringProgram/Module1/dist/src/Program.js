import { BinaryTreeFactory, RedBlackBinaryTreeFactory, RedBlueBinaryTreeFactory } from "./Factory.js";
import { Utilities } from "./Utilities.js";

let config;
await fetch("./../config.json")
    .then(response => response.json())
    .then(data => config = data);

const program = (function(property) {

    let timer;
    console.log(`The number of items is: ${config.numberOrItems}`);
    console.log(`The comparator property is: ${property}`);

    let comparator = (d1, d2) => {
        if (d1[property] > d2[property]) return 1;
        else if (d1[property] < d2[property]) return -1;
        return 0;
    };

    // Input build
    timer = Utilities.StopWatch("build input").start();
    let input = [];
    for (let i = 0; i < config.numberOrItems; i++) {
        input.push(Utilities.GetRandomInputObj(config.properties));        
    }
    timer.stopAndShow();

    console.log("");

    let binaryTree = new BinaryTreeFactory(comparator);
    let redBlackBinaryTree = new RedBlackBinaryTreeFactory(comparator);
    let redBlueBinaryTree = new RedBlueBinaryTreeFactory(comparator);

    // Inserts
    binaryTree.insertWithTimeMeasured(input);
    redBlackBinaryTree.insertWithTimeMeasured(input);
    redBlueBinaryTree.insertWithTimeMeasured(input);

    console.log("");

    // Searches
    binaryTree.inorderWithTimeMeasured();
    redBlackBinaryTree.inorderWithTimeMeasured();
    redBlueBinaryTree.inorderWithTimeMeasured();
});

config.propertiesToRunAgainst.forEach(property => {    
    program(property);
    console.log("\n\n");
});