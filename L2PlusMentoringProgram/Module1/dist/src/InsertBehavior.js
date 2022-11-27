
export class InsertBehavior
{
    constructor()
    {
        if ( this.constructor == InsertBehavior )
        {
            throw new Error( "Abstract classes can't be instantiated." );
        }
    }

    perform ( data, tree, comparatorFirstIsGreater )
    {
        throw new Error( "Method 'perform' must be implemented." );
    }
}
