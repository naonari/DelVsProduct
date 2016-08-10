using System;
using System.IO;

namespace DelVsProduct
{
    static class Program
    {

        /// <summary>ルートパス。</summary>
        private static readonly string ROOT_PATH = Directory.GetCurrentDirectory();

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // プロダクトディレクトリを削除します。
            DeleteProductDirectory(ROOT_PATH);
        }

        // プロダクトの中間ファイル格納ディレクトリ名。
        private static readonly string OBJ_DIRECTORY_LOWER_NAME = "obj";

        // プロダクトの成果物ファイル格納ディレクトリ名。
        private static readonly string BIN_DIRECTORY_LOWER_NAME = "bin";

        // 実行ファイルの拡張子。
        private static readonly string EXECUTE_EXTENSION = ".exe";

        /// <summary>
        /// プロダクトディレクトリを削除します。
        /// </summary>
        /// <param name="rootPath">対象のルートディレクトリのパス。</param>
        private static void DeleteProductDirectory(string rootPath)
        {
            // ファイルを走査します。
            foreach (var filePath in Directory.GetFiles(rootPath))
            {
                // 最上位ディレクトリの場合は走査処理を終了します。
                if (ROOT_PATH.Equals(rootPath)) break;

                if (EXECUTE_EXTENSION.Equals(Path.GetExtension(filePath)))
                {
                    // ファイルを削除します。
                    ForceDelete(filePath);
                }
            }

            // ルートディレクトリのサブディレクトリにて走査します。
            foreach (var path in Directory.GetDirectories(rootPath))
            {
                // ディレクトリ名を取得します。
                var dirName = Path.GetFileName(path).ToLower();

                // 削除対象のディレクトリかを判定します。
                if (OBJ_DIRECTORY_LOWER_NAME.Equals(dirName) || BIN_DIRECTORY_LOWER_NAME.Equals(dirName))
                {
                    // ディレクトリを削除します。
                    Directory.Delete(path, true);
                    continue;
                }

                // 再帰的にディレクトリを削除します。
                DeleteProductDirectory(path);
            }
        }

        /// <summary>
        /// ファイルを削除します。
        /// </summary>
        /// <param name="filePath">削除するファイルのパス。</param>
        private static void ForceDelete(string filePath)
        {
            // ファイル情報クラスをインスタンス化します。
            var fi = new FileInfo(filePath);

            // ファイルが存在するかを判定します。
            if (fi.Exists)
            {
                // 読み取り専用属性がある場合は、読み取り専用属性を解除します。
                if ((fi.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    fi.Attributes = FileAttributes.Normal;
                }

                // ファイルを削除します。
                fi.Delete();
            }
        }
    }
}
