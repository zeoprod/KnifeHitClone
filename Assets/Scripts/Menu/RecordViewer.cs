using Gameplay.Repositories.HighScoreRepository;
using TMPro;
using UnityEngine;
using Zenject;

namespace Menu
{
	public class RecordViewer : MonoBehaviour
	{
		[SerializeField] private TMP_Text _recordValue;

		private IRecordRepository _recordRepository;

		[Inject]
		public void Construct(IRecordRepository recordRepository)
		{
			_recordRepository = recordRepository;
		}

		private void Start()
		{
			_recordValue.text = $"Record: {_recordRepository.GetRecord()}";
		}
	}
}