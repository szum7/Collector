import { BinaryTreeBase } from "./BinaryTreeBase.js";
import { Insert } from "./Insert.js";

export class BinaryTree extends BinaryTreeBase {

    constructor(comparator) {
        super(comparator);

        this.setInsertBehavior(new Insert());
    }

    getNameOfImplementingClass() {
        return this.constructor.name;
    }

    show() {
        console.log("BinaryTree.");
    }
}