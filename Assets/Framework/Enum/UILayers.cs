namespace GameFramework
{
    //模式 小于等于Page层级的，会单一存在，及开启一个将会关闭所有同等级的
    // 剩下的一律为可重叠存在
    public enum UILayers
    {
        //基础
        Base = 0,
        //背景
        Backgroud = 10,
        //页面
        Page = 20,
        //窗口
        Window = 30,
        //窗口
        NoMaskWindow = 40,
        //消息
        Message = 50,
        //弹窗
        Popup = 100,
        //紧急
        Emergent = 500,
        //王，通吃所有层
        King = 1000,
    }
}