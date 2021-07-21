// using MessagePipe;
// using UnityEngine;
// using Zenject;

// namespace PPD
// {
//     public class ZenjectInstaller : MonoInstaller
//     {
//         public override void InstallBindings()
//         {
//             // MessagePipeをZenjectContainerにバインド
//             var option = Container.BindMessagePipe();

//             // PlayerAttackDataのイベントを登録
//             Container.BindMessageBroker<PlayerAttackData>(option);
//         }
//     }
// }
