
export class BinaryNode
{

    constructor( data )
    {
        this.data = data;
        this.left = null;
        this.right = null;
    }

    setChild ( dir, val )
    {
        if ( dir )
        {
            this.right = val;
        } else
        {
            this.left = val;
        }
    }

    getChild ( dir )
    {
        return dir ? this.right : this.left;
    }
}
