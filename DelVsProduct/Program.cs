using System;
using System.IO;

namespace DelVsProduct
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // プロダクトディレクトリを削除します。
            DeleteProductDirectory(Directory.GetCurrentDirectory());
        }

        // プロダクトの中間ファイル格納ディレクトリ名。
        private static readonly string OBJ_DIRECTORY_LOWER_NAME = "obj";

        // プロダクトの成果物ファイル格納ディレクトリ名。
        private static readonly string BIN_DIRECTORY_LOWER_NAME = "bin";

        /// <summary>
        /// プロダクトディレクトリを削除します。
        /// </summary>
        /// <param name="rootPath">対象のルートディレクトリのパス。</param>
        private static void DeleteProductDirectory(string rootPath)
        {
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
    }
}
