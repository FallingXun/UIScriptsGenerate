-- 功能：-- 作者：
-- 初始化
function TestSubScreen:Init()
    self:OnSpawn();
end

-- 销毁
function TestSubScreen:Dispose()
    self:OnRelease();
end

-- 提取
function TestSubScreen:OnSpawn()
    -- 注册UI监听
    self:RegisterUI();
    -- 注册事件监听
    self:RegisterFevent();
end

-- 回收
function TestSubScreen:OnRelease()
    -- 移除事件监听
    self:UnRegisterFevent();
end

-- UI事件注册
function TestSubScreen:RegisterUI()

end

-- 消息事件注册
function TestSubScreen:RegisterFevent()

end

-- 消息事件取消注册
function TestSubScreen:UnRegisterFevent()

end

-- 方法自动生成位置，请勿删除此行 --

