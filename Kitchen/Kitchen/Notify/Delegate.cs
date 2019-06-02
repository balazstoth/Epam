namespace Kitchen
{
    public delegate void OvenIsFinishedDelegate(object sender, OvenFinishedEventArgs e);
    public delegate void IngredientIsPreparedDelegate<T>(object sender, IngredienIsPreparedEventArgs<T> e);
}
