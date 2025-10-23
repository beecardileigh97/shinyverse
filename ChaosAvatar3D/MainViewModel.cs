using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace ChaosAvatar3D
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private Model3D? _avatarModel;

        [ObservableProperty]
        private bool _isLoading = true;

        [ObservableProperty]
        private string _userInput = string.Empty;

        [ObservableProperty]
        private bool _isSettingsVisible;

        public ObservableCollection<string> ChatHistory { get; } = new ObservableCollection<string>();

        private readonly OpenAiService _openAiService;
        private readonly SpeechSynthesizer _speechSynthesizer;
        private readonly string _systemPrompt;
        private readonly Dictionary<string, object> _knowledgeBase;

        public MainViewModel()
        {
            _openAiService = new OpenAiService();
            _speechSynthesizer = new SpeechSynthesizer();

            (string systemPrompt, Dictionary<string, object> knowledgeBase) = LoadKnowledgeBase();
            _systemPrompt = systemPrompt;
            _knowledgeBase = knowledgeBase;

            LoadAvatarAsync();
        }

        private (string, Dictionary<string, object>) LoadKnowledgeBase()
        {
            var path = "pokemon_go_knowledge.json";
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var knowledge = JsonConvert.DeserializeObject<dynamic>(json);
                string prompt = knowledge.system_prompt;
                var kb = JsonConvert.DeserializeObject<Dictionary<string, object>>(knowledge.knowledge_base.ToString());
                return (prompt, kb);
            }
            return ("You are a helpful assistant.", new Dictionary<string, object>());
        }

        private async void LoadAvatarAsync()
        {
            IsLoading = true;
            
            await Task.Run(() =>
            {
                var eeveeGroup = new Model3DGroup();
                
                // Colors
                var brown = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(139, 105, 65))); // SaddleBrown
                var cream = new DiffuseMaterial(new SolidColorBrush(Color.FromRgb(245, 222, 179))); // Wheat
                var black = new DiffuseMaterial(new SolidColorBrush(Colors.Black));

                // Body
                var bodyBuilder = new MeshBuilder(false, false);
                bodyBuilder.AddBox(new Point3D(0, 0, 0), 1.5, 0.8, 1);
                eeveeGroup.Children.Add(new GeometryModel3D(bodyBuilder.ToMesh(), brown));

                // Head
                var headBuilder = new MeshBuilder(false, false);
                headBuilder.AddBox(new Point3D(1.1, 0.2, 0), 0.8, 0.7, 0.7);
                eeveeGroup.Children.Add(new GeometryModel3D(headBuilder.ToMesh(), brown));

                // Ears
                var earsBuilder = new MeshBuilder(false, false);
                earsBuilder.AddPyramid(new Point3D(1.1, 0.55, 0.3), 0.3, 0.8); // Right Ear
                earsBuilder.AddPyramid(new Point3D(1.1, 0.55, -0.3), 0.3, 0.8); // Left Ear
                eeveeGroup.Children.Add(new GeometryModel3D(earsBuilder.ToMesh(), brown));

                // Tail
                var tailBuilder = new MeshBuilder(false, false);
                tailBuilder.AddSphere(new Point3D(-1.1, 0.2, 0), 0.5);
                eeveeGroup.Children.Add(new GeometryModel3D(tailBuilder.ToMesh(), cream));
                
                // Eyes
                var eyesBuilder = new MeshBuilder(false, false);
                eyesBuilder.AddSphere(new Point3D(1.45, 0.3, 0.2), 0.08); // Right Eye
                eyesBuilder.AddSphere(new Point3D(1.45, 0.3, -0.2), 0.08); // Left Eye
                eeveeGroup.Children.Add(new GeometryModel3D(eyesBuilder.ToMesh(), black));

                // Nose
                var noseBuilder = new MeshBuilder(false, false);
                noseBuilder.AddSphere(new Point3D(1.5, 0.15, 0), 0.05);
                eeveeGroup.Children.Add(new GeometryModel3D(noseBuilder.ToMesh(), black));

                // Neck Fluff
                var fluffBuilder = new MeshBuilder(false, false);
                fluffBuilder.AddTorus(0.5, 0.2, 32, 32);
                var fluffModel = new GeometryModel3D(fluffBuilder.ToMesh(), cream)
                {
                    Transform = new TranslateTransform3D(0.5, 0, 0)
                };
                eeveeGroup.Children.Add(fluffModel);

                eeveeGroup.Freeze();
                AvatarModel = eeveeGroup;
            });
            
            IsLoading = false;
        }

        [RelayCommand]
        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(UserInput)) return;

            var userMessage = UserInput;
            ChatHistory.Add($"You: {userMessage}");
            UserInput = string.Empty;
            IsLoading = true;

            var messages = new List<object>
            {
                new { role = "system", content = _systemPrompt },
                new { role = "system", content = $"Here is a knowledge base of current and future events in JSON format: {JsonConvert.SerializeObject(_knowledgeBase)}" },
                new { role = "user", content = userMessage }
            };

            var aiResponse = await _openAiService.GetChatCompletionAsync(messages.ToArray());

            ChatHistory.Add($"Ember: {aiResponse}");
            IsLoading = false;

            _speechSynthesizer.SpeakAsync(aiResponse);
        }

        [RelayCommand]
        private void ToggleSettings()
        {
            IsSettingsVisible = !IsSettingsVisible;
        }

        [RelayCommand]
        private void ClearChat()
        {
            ChatHistory.Clear();
        }

        [RelayCommand]
        private void CloseWindow()
        {
            Application.Current.Shutdown();
        }
    }
}
