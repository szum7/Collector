import { BinaryNode } from "./BinaryNode.js";


export class ColoredBinaryNode extends BinaryNode
{

    constructor( data, isRed )
    {
        super( data );
        this.isRed = isRed;
    }
}
