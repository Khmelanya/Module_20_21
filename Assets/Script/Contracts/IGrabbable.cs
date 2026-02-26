
public interface IGrabbable
{
    float FollowSpeed { get; }

    void OnGrab(GrabContext context);
    void OnRelease(GrabContext context);
}
