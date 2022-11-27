import { BinaryTree } from "./BinaryTree.js";
import { RedBlackBinaryTree } from "./RedBlackBinaryTree.js";
import { RedBlueBinaryTree } from "./RedBlueBinaryTree.js";
import { Utilities } from "./Utilities.js";

class Factory {

    constructor(comparator){
        if (this.constructor == Factory) {
            throw new Error("Abstract classes can't be instantiated.");
        }
        this._comparator = comparator;
        this._instance = this.getInstance();
    }

    getInstance() {
        throw new Error("");
    }

    insertWithTimeMeasured(input) {
        let self = this;
        let timer = Utilities.StopWatch(`${self._instance.getNameOfImplementingClass()} insert`).start();
        input.forEach(item => {
            self._instance.performInsert(item);
        }); 
        timer.stopAndShow();
    }

    inorderWithTimeMeasured() {
        let timer = Utilities.StopWatch(`${this._instance.getNameOfImplementingClass()} inorder`).start();
        this._instance.inorder(function(data){ ; });
        timer.stopAndShow();
    }
}

export class BinaryTreeFactory extends Factory {

    constructor(comparator){
        super(comparator);
    }

    getInstance() {
        return new BinaryTree(this._comparator);
    }
}

export class RedBlackBinaryTreeFactory extends Factory {

    constructor(comparator){
        super(comparator);
    }

    getInstance() {
        return new RedBlackBinaryTree(this._comparator);
    }
}

export class RedBlueBinaryTreeFactory extends Factory {

    constructor(comparator){
        super(comparator);
    }

    getInstance() {
        return new RedBlueBinaryTree(this._comparator);
    }
}
