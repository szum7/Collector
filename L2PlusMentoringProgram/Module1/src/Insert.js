import { InsertBehavior } from "./InsertBehavior.js";
import { BinaryNode } from "./models/BinaryNode.js";

export class Insert extends InsertBehavior {

    perform ( data, tree, comparatorFirstIsGreater )
    {
        var newNode = new BinaryNode( data );

        if ( tree.root === null )
            tree.root = newNode;

        else
            this._insertNode( tree.root, newNode, comparatorFirstIsGreater );
    }

    _insertNode ( node, newNode, comparatorFirstIsGreater )
    {
        if ( comparatorFirstIsGreater( node.data, newNode.data ) === 1 )
        {
            if ( node.left === null )
                node.left = newNode;

            else
                this._insertNode( node.left, newNode, comparatorFirstIsGreater );
        } else
        {
            if ( node.right === null )
                node.right = newNode;

            else
                this._insertNode( node.right, newNode, comparatorFirstIsGreater );
        }
    }
}
