# ShoppingMemo

買い物リストを登録・表示・更新・削除できるWebアプリケーションです。

## 使用技術

- C#
- ASP.NET Core Razor Pages
- .NET 10
- MySQL
- MySqlConnector
- Docker Compose

## データベース設計

### データベース概要

| 項目 | 内容 |
|---|---|
| データベース | MySQL |
| データベース名 | shoppingmemo |
| テーブル名 | ShoppingItems |

### テーブル定義：ShoppingItems

| カラム名 | データ型 | 制約 | 説明 |
|---|---|---|---|
| Id | INT | PRIMARY KEY、AUTO_INCREMENT | 買い物メモのID |
| Name | TEXT | NOT NULL | 買い物メモの内容 |

### テーブル間の関係

現在のデータベースは `ShoppingItems` テーブルのみで構成されています。

そのため、テーブル間のリレーション（外部キーによる関連付け）はありません。

## 起動方法

### 1. MySQLの起動

Docker Desktopを起動し、プロジェクトのルートディレクトリで以下を実行します。

```powershell
docker compose up -d
```

### 2. アプリケーションの起動

```powershell
dotnet restore
dotnet run
```

### 3. ブラウザでアクセス

ターミナルに表示されたURLをブラウザで開きます。

## 環境変数の設定

プロジェクトのルートディレクトリにある `.env.example` をコピーして `.env` を作成します。

```powershell
Copy-Item .env.example .env
```

`.env` を開き、MySQLのパスワードを設定してください。

`.env` には実際のパスワードを記載するため、GitHubには公開しないでください。

`.env.example` は設定項目の見本として使用します。