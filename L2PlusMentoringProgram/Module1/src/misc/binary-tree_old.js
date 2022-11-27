console.log("homework.js.");

class BinaryNode {

    constructor(data) {
        this.data = data;
        this.left = null;
        this.right = null;
    }

    setChild(dir, val) {
        if (dir) {
            this.right = val;
        } else {
            this.left = val;
        }
    }

    getChild(dir) {
        return dir ? this.right : this.left;
    }
}

class RedBlackBinaryNode extends BinaryNode {   

    constructor(data, isRed) {
        super(data);
        this.isRed = isRed;
    }
}

class BinaryTreeMap {

    constructor(comparator) {
        this.root = null;
        this.comparator = comparator;
    }
    
    insert(data) {
        if (this.root === null) {
            this.root = new BinaryNode(data);
            return true;
        }
    
        var dir = 0;        
        var parent = null;
        var node = this.root;
    
        // search down
        while (true) {
            if (node === null) {
                node = new BinaryNode(data);
                parent.setChild(dir, node);
                return true;
            }
    
            // stop if found
            if (this.comparator(node.data, data) === 0) {
                return false;
            }
    
            dir = this.comparator(node.data, data) < 0;
    
            // update helpers
            parent = node;
            node = node.getChild(dir);
        }
    }
    
    inorder(node) {
        if(node !== null)
        {
            this.inorder(node.left);
            console.log(node.data);
            this.inorder(node.right);
        }
    }

    show() {
        console.log("BinaryTree");
    }
}

class RedBlackTreeMap extends BinaryTreeMap {

    constructor(comparator) {
        super(comparator);
        this.size = 0;
    }

    insert(data) {
        var ret = false;
    
        if (this.root === null) {
            this.root = new RedBlackBinaryNode(data);
            ret = true;
            this.size++;
        } else {
            var fakeRoot = new RedBlackBinaryNode(undefined);
    
            var dir = 0;
            var last = 0;
    
            // setup
            var grandparent = null; // grandparent
            var grandgrandparent = fakeRoot; // grand-grand-parent
            var parent = null; // parent
            var node = this.root;
            grandgrandparent.right = this.root;

            while (true) {
                if (node === null) {
                    // insert new node at the bottom
                    node = new RedBlackBinaryNode(data);
                    parent.setChild(dir, node);
                    ret = true;
                    this.size++;
                } else if (node.left && node.left && node.left.isRed && node.right.isRed) {
                    // color flip
                    node.isRed = true;
                    node.left.isRed = false;
                    node.right.isRed = false;
                }
    
                // fix red violation
                if (node && parent && node.isRed && parent.isRed) {
                    var dir2 = grandgrandparent.right === grandparent;
    
                    if (node === parent.getChild(last)) {
                        grandgrandparent.setChild(dir2, _singleRotate(grandparent, !last));
                    } else {
                        grandgrandparent.setChild(dir2, _doubleRotate(grandparent, !last));
                    }
                }
    
                var cmp = this.comparator(node.data, data);
    
                // stop if found
                if (cmp === 0) {
                    break;
                }
    
                last = dir;
                dir = cmp < 0;
    
                // update helpers
                if (grandparent !== null) {
                    grandgrandparent = grandparent;
                }
                grandparent = parent;
                parent = node;
                node = node.getChild(dir);
            }
    
            // update root
            this.root = fakeRoot.right;
        }
    
        // make root black
        this.root.isRed = false;
    
        return ret;
    }

    _singleRotate(root, dir) {
        var save = root.getChild(!dir);
    
        root.setChild(!dir, save.getChild(dir));
        save.setChild(dir, root);
    
        root.red = true;
        save.red = false;
    
        return save;
    }

    _doubleRotate(root, dir) {
        root.setChild(!dir, _singleRotate(root.getChild(!dir), !dir));
        return _singleRotate(root, dir);
    }

    show() {
        console.log("RedBlackTreeMap");
    }
}

let binaryTree = new BinaryTreeMap();
let redBlackTree = new RedBlackTreeMap();

binaryTree.show();
redBlackTree.show();

// sortByName
// sortBySalary