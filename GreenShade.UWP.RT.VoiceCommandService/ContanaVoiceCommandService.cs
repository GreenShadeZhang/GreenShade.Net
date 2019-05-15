using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace GreenShade.UWP.RT.VoiceCommandService
{
    public sealed class ContanaVoiceCommandService:IBackgroundTask
    {
        BackgroundTaskDeferral serviceDeferral;
        VoiceCommandServiceConnection voiceServiceConnection;
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();
            taskInstance.Canceled += OnTaskCanceled;
            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;
            if (triggerDetails != null && triggerDetails.Name == "ContanaVoiceCommandService")
            {
                try
                {
                    voiceServiceConnection = VoiceCommandServiceConnection.FromAppServiceTriggerDetails(triggerDetails);
                    voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;
                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();
                    switch (voiceCommand.CommandName)
                    {
                        case "dosomething":

                            var userMessage = new VoiceCommandUserMessage();
                            var destinationsContentTiles = new List<VoiceCommandContentTile>();
                            var destinationTile = new VoiceCommandContentTile();

                            // To handle UI scaling, Cortana automatically looks up files with FileName.scale-<n>.ext formats based on the requested filename.
                            // See the VoiceCommandService\Images folder for an example.
                            destinationTile.ContentTileType = VoiceCommandContentTileType.TitleWith68x68IconAndText;


                            destinationTile.AppLaunchArgument = "牛逼";
                            destinationTile.Title = "傻瓜";
                            userMessage.DisplayMessage = "牛逼";
                            userMessage.SpokenMessage = "牛逼了你";
                            destinationsContentTiles.Add(destinationTile);
                            var response = VoiceCommandResponse.CreateResponse(userMessage, destinationsContentTiles);
                            await voiceServiceConnection.ReportSuccessAsync(response);
                            break;
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
                }

            }
        }
        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Task cancelled, clean up");
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }
    }
}
