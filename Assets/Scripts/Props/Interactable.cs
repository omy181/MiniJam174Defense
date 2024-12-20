public interface Interactable
{
    public void Interract(Player player);
    public void StopInterract(Player player);
    public bool IsInteractable(Player player);
}

public interface IngameEvent
{
    public float EventProbability();
    public void ActEvent();
}

public interface Collidable
{
    public void OnCollided(Player player);
}
