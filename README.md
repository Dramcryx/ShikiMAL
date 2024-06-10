# ShikiMALSynchronizer - �������� ���� Shikimori � ������� MyAnimeList (MAL)

���� ��� ������ ���������:
1) ������������ � MAL � ����� ������ ������������� �����;
2) ���� �����, ��� � 1), ������ ��� Shikimori;
3) ����������, ���� �� ������� � MAL ��� ��� ����, �� ���������� �� �������, ������ ��� ���������� ��������;
4) ���������� ������� � ������� MAL.

������� ��:
1) Common - ����� ���������� ��� �������� ����� ��� �����;
2) MyAnimeListClient - ���������������� API ��� MAL, �������������� ������������� ��, ��� ����� ����������;
3) ShikimoriClient - ����� �� ������ ��� Shikimori;
4) ShikiMALSynchronizer - �������� ������ ����������, ������������ � ���� ������� � ���������� ������������.

������:
1) Shikimori ��� ������� ����� ��������� ����� ����� ����������� ������ ������� ������, � ������� �� ������� ���� `myanimelist_id`. ��-�� ����� ���������� ����������� ������ �� ������� �� ������ ��� � �������, ��� ���� ����� ��������;
2) MyAnimeList ������ 429 ��� ����������� ��� ������� ���������� ������ ������� � ������� �� API Gateway, ��-�� ���� ���������� ����� ������ ������ �� ������ �������. ��������� ��� ��� �������� �� �������, ����������� ��������� ��-�������.

���������:
1) Rate limiter �� �������;
2) ���������� ���������� ������.