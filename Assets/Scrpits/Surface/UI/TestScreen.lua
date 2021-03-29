-- 功能：-- 作者：
-- UI资源加载完成回调，UI关闭前再调用OpenUI不会再调用
function TestScreen:OnLoadSuccess()
    -- 注册UI监听
    self:RegisterUI();
    -- 注册事件监听
    self:RegisterFevent();
end

-- UI初始化，每次调用OpenUI都会执行
function TestScreen:OnInit()

end

-- UI销毁，可做事件注销
function TestScreen:Dispose()

end

-- UI关闭
function TestScreen:OnClose()

end

-- UI点击背景遮罩事件，默认执行OnClose方法关闭UI
function TestScreen:OnClickMaskArea()
    self:OnClose();
end

-- UI事件注册
function TestScreen:RegisterUI()

end

-- 消息事件注册
function TestScreen:RegisterFevent()

end

-- 方法自动生成位置，请勿删除此行 --

