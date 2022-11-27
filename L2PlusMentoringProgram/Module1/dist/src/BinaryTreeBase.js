export class BinaryTreeBase {

    constructor(comparator) {
        if (this.constructor == BinaryTreeBase) {
            throw new Error("Abstract classes can't be instantiated.");
        }

        if (typeof comparator !== "function") {
            throw new Error("'comparator' is not a function!");
        }

        this.root = null;
        this.comparator = comparator;

        this.insertBehavior = null;
    }

    setInsertBehavior(behavior) {
        this.insertBehavior = behavior;
    }

    performInsert(data) {
        this.insertBehavior.perform(data, this, this.comparator);
    }

    search(data) {
        this._recursiveSearch(this.root, data);
    }

    _recursiveSearch(node, data) {
        if (node === null)
            return null;
        else if (this.comparator(node.data, data) === 1)
            return this._recursiveSearch(node.left, data);
        else if (this.comparator(node.data, data) === -1)
            return this._recursiveSearch(node.right, data);
        else
            return node;
    }

    preorder(operation) {
        this._preorder(this.root, operation);
    }

    _preorder(node, operation) {
        if (node !== null) {
            operation(node.data);
            this._preorder(node.left, operation);
            this._preorder(node.right, operation);
        }
    }

    inorder(operation) {
        this._inorder(this.root, operation);
    }

    _inorder(node, operation) {
        if (node !== null) {
            this._inorder(node.left, operation);
            operation(node.data);
            this._inorder(node.right, operation);
        }
    }

    postorder(node, operation) {
        if (node !== null) {
            this.postorder(node.left, operation);
            this.postorder(node.right, operation);
            operation(node.data);
        }
    }

    getNameOfImplementingClass() {
        throw new Error("Method 'getNameOfImplementingClass' must be implemented.");
    }

    show() {
        throw new Error("Method 'show()' must be implemented.");
    }
}