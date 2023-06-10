import DebugContext from "./Foudation/unity_editor_context.mjs";
console.log("Start Js Env");
                                                                                             
function InitEnv(context: Typescript.Runtime.JsContext) {
    console.log(context)
    console.log(DebugContext.OnUpdate)
    context.OnUpdate = () => {
        DebugContext.OnUpdate()
    }
}

export default InitEnv
