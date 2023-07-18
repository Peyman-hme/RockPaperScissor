
public interface IClientIDSubscriber
{
    void Update(int id);
}

public interface IRivalIDSubscriber
{
    void Update(int id);
}

public interface IDrawnCardSubscriber
{
    void Update(int cardID,int playerID);
}

