import { expect } from "chai";
import { BinaryTree } from "./BinaryTree.js";

describe("BinaryTree", () => {

    let tree, returnedNodes, operation;

    beforeEach(() => {
        tree = new BinaryTree((data1, data2) => {
            if (data1 > data2) return 1;
            else if (data1 < data2) return -1;
            else return 0;
        });      
        returnedNodes = [];
        operation = (data) => {
            returnedNodes.push(data);
        };
    });

    describe("getNameOfImplementingClass", () => {
        it("returns the name of the class.", () => {
            const expected = "BinaryTree";
            const actual = tree.getNameOfImplementingClass();
            expect(actual).to.equal(expected);
        });
    });

    describe("inorder", () => {
        it("empty tree returns null.", () => {            
            // Act
            tree.inorder(operation);
    
            // Assert
            expect(returnedNodes.length).to.equal(0);
        });
    });

    describe("inorder", () => {
        it("binary tree with one root node returns the root's data.", () => {            
            // Act
            tree.performInsert("nodeDataValue");
            tree.inorder(operation);
    
            // Assert
            expect(returnedNodes[0]).to.equal("nodeDataValue");
        });
    });

    describe("inorder", () => {
        it("nodes are returned in ascending order.", () => {
            // Arrange
            let isInOrder = true;
            
            // Act
            tree.performInsert(3);
            tree.performInsert(2);
            tree.performInsert(7);
            tree.performInsert(5);
            tree.performInsert(1);
            tree.performInsert(6);
            tree.performInsert(6);
            tree.performInsert(4);
    
            tree.inorder(operation);
    
            for (let i = 0; i < returnedNodes.length - 1; i++) {
                if (returnedNodes[i] > returnedNodes[i + 1]){
                    isInOrder = false;
                    break;
                }
            }
             
            // Assert
            expect(isInOrder).to.equal(true);
        });
    });
});