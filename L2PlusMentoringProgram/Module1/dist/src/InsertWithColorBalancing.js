import { InsertBehavior } from "./InsertBehavior.js";
import { ColoredBinaryNode } from "./models/ColoredBinaryNode.js";

export class InsertWithColorBalancing extends InsertBehavior
{
    perform ( data, tree, comparatorFirstIsGreater )
    {
        if ( tree.root === null )
        {
            tree.root = new ColoredBinaryNode( data );
        } else {
            var fakeRoot = new ColoredBinaryNode( undefined );

            var dir = 0;
            var last = 0;

            // setup
            var grandparent = null;
            var grandgrandparent = fakeRoot;
            var parent = null;
            var node = tree.root;
            grandgrandparent.right = tree.root;

            while ( true )
            {
                if ( node === null )
                {
                    node = new ColoredBinaryNode( data );
                    parent.setChild( dir, node );
                } else if ( node.left && node.left && node.left.isRed && node.right.isRed )
                {
                    // color flip
                    node.isRed = true;
                    node.left.isRed = false;
                    node.right.isRed = false;
                }

                // fix red violation
                if ( node && parent && node.isRed && parent.isRed )
                {
                    var dir2 = grandgrandparent.right === grandparent;

                    if ( node === parent.getChild( last ) )
                    {
                        grandgrandparent.setChild( dir2, this._singleRotate( grandparent, !last ) );
                    } else
                    {
                        grandgrandparent.setChild( dir2, this._doubleRotate( grandparent, !last ) );
                    }
                }

                var cmp = comparatorFirstIsGreater( node.data, data );

                // stop if found
                if ( cmp === 0 )
                {
                    break;
                }

                last = dir;
                dir = cmp < 0;

                // update helpers
                if ( grandparent !== null )
                {
                    grandgrandparent = grandparent;
                }
                grandparent = parent;
                parent = node;
                node = node.getChild( dir );
            }

            // update root
            tree.root = fakeRoot.right;
        }

        // make root black
        tree.root.isRed = false;
    }

    _singleRotate ( root, dir )
    {
        var save = root.getChild( !dir );

        root.setChild( !dir, save.getChild( dir ) );
        save.setChild( dir, root );

        root.red = true;
        save.red = false;

        return save;
    }

    _doubleRotate ( root, dir )
    {
        root.setChild( !dir, this._singleRotate( root.getChild( !dir ), !dir ) );
        return this._singleRotate( root, dir );
    }
}
